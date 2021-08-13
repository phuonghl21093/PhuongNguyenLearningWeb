//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('banner', ['$scope', '$http', '$controller', '$timeout', 'articleService',
  function ($scope, $http, $controller, $timeout, articleService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.service = articleService;

      $scope.formViewTemplate = '/Angular/Views/Admin/ImageBanner/detail.html';

      $scope.dataFilter = {
          name: '',
          typeId: 55,
          groupId: null,
          currentRecord: 0,
          numberRecord: $scope.listNumberRow[0],
          currentPage: 1,
          propertyNameSort: 'ID',
          descending: true
      };

      $scope.toggleTranslate = function(e, id) {
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

              $scope.openModal('/Angular/Views/Admin/ImageBanner/edit.html');

          });
      };

      $scope.translate = function (id, languageId) {
          articleService.getEditForm(id, languageId).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              $scope.listArticleGroup = res.listGroup;

              $scope.openModal('/Angular/Views/Admin/ImageBanner/edit.html');

          });
      }

      $scope.loadPropertyFilter();
      $scope.loadData();

  }]);

'use strict';
web365app.controller('bannerModified', ['$scope', '$http', '$controller', 'articleService',
  function ($scope, $http, $controller, articleService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          for (var instanceName in CKEDITOR.instances) {
              CKEDITOR.instances[instanceName].updateElement();
          }

          $scope.dataForm = $(form.target).serialize();
          $scope.dataForm.TypeId = 55;

          articleService.modified($scope.dataForm).then(function (res) {

              $scope.resetList();

              $scope.cancel();
          });
      };

  }]);