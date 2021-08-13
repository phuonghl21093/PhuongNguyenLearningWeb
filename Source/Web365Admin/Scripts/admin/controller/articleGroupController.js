//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('articleGroup', ['$scope', '$http', '$controller', '$timeout', 'articleGroupService',
  function ($scope, $http, $controller, $timeout, articleGroupService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));      

      $scope.service = articleGroupService;

      $scope.formViewTemplate = '/Angular/Views/Admin/ArticleGroup/detail.html';

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
          articleGroupService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.openModal('/Angular/Views/Admin/ArticleGroup/edit.html');

          });
      };

      $scope.translate = function (id, languageId) {
          articleGroupService.getEditForm(id, languageId).then(function (res) {

              $scope.data = res.data;

              $scope.openModal('/Angular/Views/Admin/ArticleGroup/edit.html');

          });
      }

      $scope.loadData();

  }]);

'use strict';
web365app.controller('articleGroupModified', ['$scope', '$http', '$controller', 'articleGroupService',
  function ($scope, $http, $controller, articleGroupService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          if ($scope.modified_form.$valid) {

              for (var instanceName in CKEDITOR.instances) {
                  CKEDITOR.instances[instanceName].updateElement();
              }

              $scope.dataForm = $(form.target).serialize();

              articleGroupService.modified($scope.dataForm).then(function(res) {

                  $scope.resetList();

                  $scope.cancel();

              });
          } else {

              utilityServices.notificationWarning('Thông báo', 'Bạn cần nhập dữ liệu cần thiết');

          }
      };

  }]);