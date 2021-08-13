//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('language', ['$scope', '$http', '$controller', '$timeout', 'languageService',
  function ($scope, $http, $controller, $timeout, languageService) {

      //extend baseController
      $controller('baseController', { $scope: $scope });
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = languageService;

      $scope.formViewTemplate = '/Angular/Views/Admin/Language/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };

      $scope.loadDataForm = function (data) {
          languageService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listLayoutGroup;

              $scope.openModal('/Angular/Views/Admin/Language/edit.html');

          });

      };

      $scope.loadData();

  }]);

'use strict';
web365app.controller('languageModified', ['$scope', '$http', '$controller', 'utilityServices', 'languageService',
  function ($scope, $http, $controller, utilityServices, languageService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          if ($scope.modified_form.$valid) {

              for (var instanceName in CKEDITOR.instances) {
                  CKEDITOR.instances[instanceName].updateElement();
              }

              $scope.dataForm = $(form.target).serialize();

              languageService.modified($scope.dataForm).then(function (res) {

                  $scope.resetList();

                  $scope.cancel();

              });
          } else {

              utilityServices.notificationWarning('Thông báo', 'Bạn cần nhập dữ liệu cần thiết');

          }
      };      

  }]);