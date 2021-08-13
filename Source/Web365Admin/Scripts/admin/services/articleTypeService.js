//create by BienLV2
//15-07-2014
//services use to product type service
'use strict';
web365app.factory('articleTypeService', ['$q', '$http', function ($q, $http) {

    var articleTypeService = {};

    //function get list product type
    var _getList = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/ArticleType/GetList', {
            params: obj
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get list product type
    var _getListOfGroup = function (groupId) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/ArticleType/GetListOfGroup', {
            params: {
                groupId: groupId
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get list for tree product type
    var _getListForTree = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/ArticleType/GetListForTree').success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get detail form picture type
    var _getDetailForm = function (id) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/ArticleType/Detail', {
            params: {
                id: id
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get edit form product type
    var _getEditForm = function (id, lang) {

        var deferred = $q.defer();

        //get data from services
        $http.get('/ArticleType/EditForm', {
            params: {
                id: id,
                language: lang
            }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    //function get edit form product type
    var _modified = function (obj) {

        var deferred = $q.defer();

        //get data from services
        $http.post('/ArticleType/Action', obj, {
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' }
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _showAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/ArticleType/ShowAll', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _hideAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/ArticleType/HideAll', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    var _deleteAll = function (listId) {

        var deferred = $q.defer();

        $http.post('/ArticleType/Delete', {
            listId: listId
        }).success(function (data) {
            deferred.resolve(data);
        }).error(function (err, status) {
            deferred.reject(err);
        });

        return deferred.promise;
    };

    articleTypeService.getList = _getList;
    articleTypeService.getListOfGroup = _getListOfGroup;
    articleTypeService.getListForTree = _getListForTree;
    articleTypeService.getDetailForm = _getDetailForm;
    articleTypeService.getEditForm = _getEditForm;
    articleTypeService.modified = _modified;
    articleTypeService.showAll = _showAll;
    articleTypeService.hideAll = _hideAll;
    articleTypeService.deleteAll = _deleteAll;

    return articleTypeService;
}]);