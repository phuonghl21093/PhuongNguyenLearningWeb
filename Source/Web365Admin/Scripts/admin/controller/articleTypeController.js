//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('articleType', ['$scope', '$http', '$controller', '$timeout', 'articleTypeService',
  function ($scope, $http, $controller, $timeout, articleTypeService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));      

      $scope.service = articleTypeService;

      $scope.formViewTemplate = '/Angular/Views/Admin/ArticleType/detail.html';

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
          articleTypeService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              //$scope.listGroup = res.listGroup;

              $scope.openModal('/Angular/Views/Admin/ArticleType/edit.html');

          });
      };

      $scope.translate = function (id, languageId) {
          articleTypeService.getEditForm(id, languageId).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              $scope.openModal('/Angular/Views/Admin/ArticleType/edit.html');

          });
      }

      $scope.loadData();

      $scope.loadDataTree();

  }]);

'use strict';
web365app.controller('articleTypeModified', ['$scope', '$http', '$controller', 'articleTypeService', 'utilityServices',
  function ($scope, $http, $controller, articleTypeService, utilityServices) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));
      $scope.submit = function (form) {     

          if ($scope.modified_form.$valid) {

              for (var instanceName in CKEDITOR.instances) {
                  CKEDITOR.instances[instanceName].updateElement();
              }

              $scope.dataForm = $(form.target).serialize();

              articleTypeService.modified($scope.dataForm).then(function (res) {

                  $scope.resetList();

                  $scope.cancel();

              });
          } else {

              utilityServices.notificationWarning('Thông báo', 'Bạn cần nhập dữ liệu cần thiết');

          }
      };



  }]);