﻿//create by BienlV
//08-08-2014 
//control view product type page
'use strict';
web365app.controller('videoType', ['$scope', '$http', '$controller', '$timeout', 'videoTypeService',
  function ($scope, $http, $controller, $timeout, videoTypeService) {

      //extend baseController
      $.extend(this, $controller('baseController', { $scope: $scope }));      

      $scope.service = videoTypeService;

      $scope.formViewTemplate = '/Angular/Views/Admin/videoType/detail.html';

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
          videoTypeService.getEditForm(data).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              $scope.openModal('/Angular/Views/Admin/videoType/edit.html');

          });
      };

      $scope.translate = function (id, languageId) {
          videoTypeService.getEditForm(id, languageId).then(function (res) {

              $scope.data = res.data;

              $scope.listTreeDataForm = res.listType;

              $scope.listArticleGroup = res.listGroup;

              $scope.openModal('/Angular/Views/Admin/videoType/edit.html');

          });
      }

      $scope.loadData();

      $scope.loadDataTree();

  }]);

'use strict';
web365app.controller('videoTypeModified', ['$scope', '$http', '$controller', 'videoTypeService',
  function ($scope, $http, $controller, videoTypeService) {

      //extend baseController
      //$.extend(this, $controller('baseController', { $scope: $scope }));

      $scope.submit = function (form) {

          for (var instanceName in CKEDITOR.instances) {
              CKEDITOR.instances[instanceName].updateElement();
          }

          $scope.dataForm = $(form.target).serialize();

          videoTypeService.modified($scope.dataForm).then(function (res) {

              $scope.resetList();

              $scope.cancel();

          });
      };

  }]);