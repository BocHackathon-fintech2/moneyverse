'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
    .controller('addBankCtrl', ['$scope', '$http', '$interval', function ($scope, $http, $interval) {
        $http.get('Boc/AuthUrl').then(function (response) {
            $scope.url = response.data.url;

            function getAccounts(type) {
                $http.get(type == 'boc' ? 'Boc/GetAccounts' : 'TrueLayer/GetAccounts').then(function (response) {
                    if(response && response.data && response.data != 'null')
                        document.location.reload()
                })
            }

            var timeouts = {};

            $scope.reloadWatch = function (type) {
                // Don't start a new fight if we are already fighting
                if (angular.isDefined(timeouts[type])) return;

                timeouts[type] = $interval(function () {
                    getAccounts(type);
                }, 2000);
            }

            $scope.stop = function () {
                if (angular.isDefined(timeouts['boc'])) {
                    $interval.cancel(timeouts['boc']);
                }
                if (angular.isDefined(timeouts['truelayer'])) {
                    $interval.cancel(timeouts['truelayer']);
                }
            };

            $scope.$on('$destroy', function () {
                $('#addBankModal').hide();
                $scope.stop();
            });
        })
    }]
);