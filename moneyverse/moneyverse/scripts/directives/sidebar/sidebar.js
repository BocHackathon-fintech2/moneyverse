'use strict';

/**
 * @ngdoc directive
 * @name izzyposWebApp.directive:adminPosHeader
 * @description
 * # adminPosHeader
 */

angular.module('sbAdminApp')
  .directive('sidebar',['$http', '$rootScope',function($http, $rootScope) {
    return {
      templateUrl:'scripts/directives/sidebar/sidebar.html',
      restrict: 'E',
      replace: true,
      scope: {
      },
        controller: function ($scope) {

            function setBanksIfNothingIsDefined() {
                    if (!$rootScope.selectedBank) {
                        $rootScope.selectedBank = $scope.banks[0];
                        $rootScope.selectedAccount = $scope.banks[0].accounts[0];
                    }
            }

            $scope.banks = [];
            $http.get('TrueLayer/GetAccounts').then(function (response) {
                if (response && response.data && response.data !== 'null') {
                    $scope.banks.push({
                        name: 'Mock Bank',
                        accounts: response.data
                    })
                    $rootScope.banks = $scope.banks;
                    setBanksIfNothingIsDefined()
                }
            })
            $http.get('Boc/GetAccounts').then(function (response) {
                if (response && response.data && response.data !== 'null') {
                    response.data.forEach(function (item) {
                        item.display_name = item.accountName;
                    })
                    $scope.banks.push({
                        name: 'Bank of Cyprus',
                        accounts: response.data
                    })
                    $rootScope.banks = $scope.banks;
                    setBanksIfNothingIsDefined()
                }
            })
        $scope.selectedMenu = 'dashboard';
        $scope.collapseVar = 0;
        $scope.multiCollapseVar = 0;
        
        $scope.check = function(x){
          
          if(x==$scope.collapseVar)
            $scope.collapseVar = 0;
          else
            $scope.collapseVar = x;
        };

            $scope.selectAccount = function (account, bank) {
                $rootScope.selectedAccount = account;
                $rootScope.selectedBank = bank;
            }
        
        $scope.multiCheck = function(y){
          
          if(y==$scope.multiCollapseVar)
            $scope.multiCollapseVar = 0;
          else
            $scope.multiCollapseVar = y;
        };
      }
    }
  }]);
