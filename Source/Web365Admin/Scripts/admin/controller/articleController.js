//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('article', ['$scope', '$http', '$controller', '$timeout', 'articleService',
  function ($scope, $http, $controller, $timeout, articleService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = articleService;

      $scope.formViewTemplate = '/Angular/Views/Admin/Article/detail.html';

      $scope.dataFilter = {
          name: '',
          typeId: null,
          groupId: null,
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
          articleService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              $scope.listArticleGroup = res.listGroup;

              $scope.openModal('/Angular/Views/Admin/Article/edit.html');

          });
      };

      $scope.translate = function (id, languageId) {
          articleService.getEditForm(id, languageId).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              $scope.listArticleGroup = res.listGroup;

              $scope.openModal('/Angular/Views/Admin/Article/edit.html');

          });
      }

      $scope.loadPropertyFilter();
      $scope.loadData();

  }]);

'use strict';
web365app.controller('articleModified', ['$scope', '$http', '$controller', 'articleService', 'utilityServices',
  function ($scope, $http, $controller, articleService, utilityServices) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {
          if ($scope.modified_form.$valid) {
              for (var instanceName in CKEDITOR.instances) {
                  CKEDITOR.instances[instanceName].updateElement();
              }

              $scope.dataForm = $(form.target).serialize();

              articleService.modified($scope.dataForm).then(function (res) {

                  $scope.resetList();

                  $scope.cancel();
              });
          } else {

              utilityServices.notificationWarning('Thông báo', 'Bạn cần nhập dữ liệu cần thiết');

          }
      };

  }]);