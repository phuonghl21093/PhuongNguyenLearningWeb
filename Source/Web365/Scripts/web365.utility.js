var web365 = web365 || {};

web365.utility = (function () {

	function toastSuccess(message) {
		toastr.success(message);
	}

	function toastWarning(message) {
		toastr.warning(message);
	}

	function toastError(message) {
		toastr.error(message);
	}

	return {
		toastSuccess: toastSuccess,
		toastWarning: toastWarning,
		toastError: toastError
	};

})();