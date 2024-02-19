$.noConflict();

jQuery(document).ready(function ($) {

	"use strict";

	new DataTable('#myDatatable');
	[].slice.call(document.querySelectorAll('select.cs-select')).forEach(function (el) {
		new SelectFx(el);
	});

	jQuery('.selectpicker').selectpicker;


	$('#menuToggle').on('click', function (event) {
		$('body').toggleClass('open');
	});

	$('.search-trigger').on('click', function (event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').addClass('open');
	});

	$('.search-close').on('click', function (event) {
		event.preventDefault();
		event.stopPropagation();
		$('.search-trigger').parent('.header-left').removeClass('open');
	});

	// $('.user-area> a').on('click', function(event) {
	// 	event.preventDefault();
	// 	event.stopPropagation();
	// 	$('.user-menu').parent().removeClass('open');
	// 	$('.user-menu').parent().toggleClass('open');
	// });



	// Change Select Value insert

	// Fetch groups for the initially selected service on page load
	var selectedServiceId = $('#serviceDropdown').val();
	fetchGroups(selectedServiceId);

	// Handle change event of service dropdown
	$('#serviceDropdown').change(function () {
		var selectedServiceId = $(this).val();
		fetchGroups(selectedServiceId);
	});
	function fetchGroups(serviceId) {
		$.ajax({
			url: '/Materiels/GetGroups',
			type: 'GET',
			data: { serviceId: serviceId },
			success: function (data) {
				// Update group dropdown with fetched groups
				$('#groupDropdown').empty();
				$.each(data, function (index, item) {
					$('#groupDropdown').append($('<option>').text(item.groupName).attr('value', item.id));
				});
			}
		});
	}



	
});

