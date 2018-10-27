"use strict";
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module("sbAdminApp").controller("DCCtrl", [
    "$scope",
    "$rootScope",
    function ($scope, $rootScope) {
        $rootScope.$watch('selectedAccount', function (n) {
            d3.json("TrueLayer/GetTransactions?accountId=" + $rootScope.selectedAccount.account_id + "&isTruelayer=" + ($rootScope.selectedBank.name != 'Bank of Cyprus'), function (data) {
            var dateFormat = d3.time.format("%Y-%m-%dT%H:%M:%S+03:00");
            var numberFormat = d3.format(".2f");
            var s = $scope;

            s.date = data.toDate;

            data.forEach(function (d) {
                d.dd = dateFormat.parse(d.timestamp);
                d.day = d3.time.day(d.dd);
                d.month = d3.time.month(d.dd);
            });

            function getTransactionType(transaction) {
                return transaction.transaction_classification[0] || "Other";
            }

            function getMerchantName(transaction) {
                return transaction.merchant_name || "Other";
            }

            // Create Crossfilter Dimensions and Groups
            var ndx = (s.ndx = crossfilter(data));
            var all = (s.all = ndx.groupAll());

            // Transactions timeline
            ///////////////////////////////////////////////////
            // dimension by day
            s.day = ndx.dimension(function (d) {
                return d.day;
            });

            // group by total volume within move, and scale down result
            s.volumeByDayGroup = s.day.group().reduceSum(function (d) {
                return 1;
            });

            // Transactions per category
            ///////////////////////////////////////////////////
            s.category = ndx.dimension(function (d) {
                return getTransactionType(d);
            });
            s.categoryGroup = s.category.group();

            // Transactions per type
            ///////////////////////////////////////////////////
            s.type = ndx.dimension(function (d) {
                return d.transaction_category;
            });
            s.typeGroup = s.type.group();

            // Transactions per merchant
            ///////////////////////////////////////////////////
            s.merchant = ndx.dimension(function (d) {
                return getMerchantName(d);
            });
            s.merchantGroup = s.merchant.group();

            // Transactions per day of the week
            ///////////////////////////////////////////////////
            s.dayOfWeek = ndx.dimension(function (d) {
                var day = d.dd.getDay();
                var name = ["Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"];
                return day + "." + name[day];
            });
            s.dayOfWeekGroup = s.dayOfWeek.group();
            s.dayOfWeekPostSetupChart = function (c) {
                c.label(function (d) {
                    return d.key.split(".")[1];
                })
                    .title(function (d) {
                        return d.value;
                    })
                    .xAxis()
                    .ticks(4);
            };

            // data table does not use crossfilter group but rather a closure
            // as a grouping function
            s.tableGroup = function (d) {
                var format = d3.format("02d");
                return d.dd.getFullYear() + "/" + format(d.dd.getMonth() + 1);
            };

            // dimension by full date
            s.dateDimension = ndx.dimension(function (d) {
                return d.dd;
            });

            s.tablePostSetupChart = function (c) {
                // dynamic columns creation using an array of closures
                c.columns([
                    function (d) {
                        return d.date;
                    },
                    function (d) {
                        return d.description;
                    },
                    function (d) {
                        return getMerchantName(d);
                    },
                    function (d) {
                        return getTransactionType(d);
                    },
                    function (d) {
                        return d.transaction_category;
                    },
                    function (d) {
                        return d.amount + " " + d.currency;
                    }
                ])
                    // (optional) sort using the given field, :default = function(d){return d;}
                    .sortBy(function (d) {
                        return d.dd;
                    })
                    // (optional) sort order, :default ascending
                    .order(d3.ascending)
                    // (optional) custom renderlet to post-process chart using D3
                    .renderlet(function (table) {
                        table.selectAll(".dc-table-group").classed("info", true);
                    });
            };


            s.resetAll = function () {
                dc.filterAll();
                dc.redrawAll();
                };
                s.resetAll();
            $scope.$apply();
        });
        })
    }
]);
