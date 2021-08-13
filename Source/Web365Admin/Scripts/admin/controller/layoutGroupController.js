//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('layoutGroup', ['$scope', '$http', '$controller', '$timeout', 'layoutGroupService',
  function ($scope, $http, $controller, $timeout, layoutGroupService) {

      //extend baseController
      $controller('baseController', { $scope: $scope });
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = layoutGroupService;

      $scope.formViewTemplate = '/Angular/Views/Admin/LayoutGroup/detail.html';

      $scope.dataFilter = {
          name: '',
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      }; 

      $scope.toggleTranslate = function (e, id) {
          $("tr." + id).toggleClass("hide");
          if ($(e.currentTarget).find("i").hasClass("icon-plus")) {
              $(e.currentTarget).find("i").removeClass("icon-plus").addClass("icon-minus");
          } else {
              $(e.currentTarget).find("i").addClass("icon-plus").removeClass("icon-minus");
          }
      }   

      $scope.loadDataForm = function (data) {

          layoutGroupService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.openModal('/Angular/Views/Admin/LayoutGroup/edit.html');

          });

      };

      $scope.translate = function (id, languageId) {
          layoutGroupService.getEditForm(id, languageId).then(function (res) {

              $scope.data = res.data;

              $scope.openModal('/Angular/Views/Admin/LayoutGroup/edit.html');

          });
      }

      $scope.loadData();

  }]);

'use strict';
web365app.controller('layoutGroupModified', ['$scope', '$http', '$controller', 'utilityServices', 'layoutGroupService',
  function ($scope, $http, $controller, utilityServices, layoutGroupService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          if ($scope.modified_form.$valid) {

              for (var instanceName in CKEDITOR.instances) {
                  CKEDITOR.instances[instanceName].updateElement();
              }

              $scope.dataForm = $(form.target).serialize();

              layoutGroupService.modified($scope.dataForm).then(function (res) {

                  $scope.resetList();

                  $scope.cancel();

              });
          } else {

              utilityServices.notificationWarning('Thông báo', 'Bạn cần nhập dữ liệu cần thiết');

          }
      };      

  }]);