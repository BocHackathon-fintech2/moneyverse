'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
    .controller('transferwiseCtrl', ['$scope', '$http', '$interval', '$rootScope', function ($scope, $http, $interval, $rootScope) {
        $scope.title = 'Login with Transferwise'
        $scope.state = 'login';
        $scope.url = 'https://sandbox.transferwise.tech/oauth/authorize?response_type=code&client_id=sapiens&redirect_uri=http://localhost:50033/Transferwise/VerifyCode'

        function getTokenActive() {
            $http.get('Transferwise/GetTokenActive').then(function (response) {
                if (response && response.data && response.data != 'false') {
                    $scope.state = 'targetaccount'
                    $scope.title = 'Select Target Account'
                    $scope.stop();
                }
            })
        }
        $scope.allAccounts = {};


        function resetAvailableAccounts() {

            $scope.allAccounts = {};
            $rootScope.banks.forEach(function (bank) {
                bank.accounts.forEach(function (account) {
                    if (bank.name != $rootScope.selectedBank.name)
                        $scope.allAccounts[account.display_name] = angular.extend(account, { bank_name: bank.name })
                })
            })
        }

        $rootScope.$watch(function () { if ($rootScope.banks) return $rootScope.banks.length; }, function (n) {
            if (n == 2) {
                resetAvailableAccounts()
            }
        })

        $rootScope.$watch('selectedBank', function (n) {
            if (n)
                resetAvailableAccounts();
        })

        var timeout;

        $scope.reloadWatch = function () {
            // Don't start a new fight if we are already fighting
            if (angular.isDefined(timeout)) return;

            timeout = $interval(function () {
                getTokenActive();
            }, 2000);
        }

        $scope.stop = function () {
            if (angular.isDefined(timeout)) {
                $interval.cancel(timeout);
            }
        };

        $scope.$on('$destroy', function () {
            $('#transferwiseModal').hide();
            $scope.stop();
        });

        $scope.getQuote = function () {
            if (!$scope.amount)
                $scope.targetAmount = undefined;
            $http.get('Transferwise/Quote?amount=' + $scope.amount).then(function (response) {
                if (response && response.data && response.data.targetAmount)
                    $scope.targetAmount = response.data.sourceAmount
            })
        }

        $scope.completeTransfer = function () {
            $http.get('Transferwise/CompleteTransfer?accountNumber=' + $scope.targetAccount.account_number.number + '&sortCode=' + $scope.targetAccount.account_number.sort_code).then(function (response) {
                $scope.state = 'transfercomplete';
                $scope.title = 'Transfer Complete';
            })
        }

        $scope.reloadWatch();


    }]
);