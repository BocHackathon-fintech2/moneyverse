'use strict';
/**
 * @ngdoc function
 * @name sbAdminApp.controller:MainCtrl
 * @description
 * # MainCtrl
 * Controller of the sbAdminApp
 */
angular.module('sbAdminApp')
    .controller('MainCtrl', function ($scope, $position, $http, $rootScope) {
        $http.get('TrueLayer/GetAccounts').then(function (response) {
            $scope.newData = response.data;
        })
  });
