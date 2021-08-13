//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('advertise', ['$scope', '$http', '$controller', '$timeout', 'advertiseService', 
  function ($scope, $http, $controller, $timeout, advertiseService, utilityServices) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));      

      $scope.service = advertiseService;

      $scope.formViewTemplate = '/Angular/Views/Admin/Advertise/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };            

      $scope.loadDataForm = function (data) {
          advertiseService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.openModal('/Angular/Views/Admin/Advertise/edit.html');

          });
      };      

      $scope.loadData();

  }]);

'use strict';
web365app.controller('advertiseModified', ['$scope', '$http', '$controller', 'advertiseService', 'utilityServices',
  function ($scope, $http, $controller, advertiseService, utilityServices) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          if ($scope.modified_form.$valid) {

              for (var instanceName in CKEDITOR.instances) {
                  CKEDITOR.instances[instanceName].updateElement();
              }

              $scope.dataForm = $(form.target).serialize();

              advertiseService.modified($scope.dataForm).then(function(res) {

                  $scope.resetList();

                  $scope.cancel();

              });
          } else {

              utilityServices.notificationWarning('Thông báo', 'Bạn cần nhập dữ liệu cần thiết');

          }
      };

  }]);