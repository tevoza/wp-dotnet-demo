function YcdSimpleCountdown()
{
	this.seconds = 0;
	this.countdownContainer = jQuery('.ycd-simple-container');
}

YcdSimpleCountdown.prototype = new YcgGeneral();

YcdSimpleCountdown.run = function()
{
	var simpleCountdown = jQuery('.ycd-simple-container');

	if (!simpleCountdown.length) {
		return false;
	}

	simpleCountdown.each(function () {
	   var options = jQuery(this).data('options');
	   var id = jQuery(this).data('id');
		var obj = new YcdSimpleCountdown();
		options['id'] = id;
		obj.options = options;
		obj.id = id;
		obj.init();
	});
};

YcdSimpleCountdown.prototype.init = function()
{
	this.render();
	this.livePreview();
};

YcdSimpleCountdown.prototype.changeDate = function() {
	var datePicker = jQuery('#ycd-date-time-picker');
	if(!datePicker.length) {
		return false;
	}

	datePicker.change(function () {
		jQuery(window).trigger('ycdChangeDate');
	})
};

YcdSimpleCountdown.prototype.changeTimeZone = function() {
	var timeZone = jQuery('.js-circle-time-zone');

	if(!timeZone.length) {
		return false;
	}

	timeZone.bind('change', function() {
		jQuery(window).trigger('ycdChangeDate');
	});
};

YcdSimpleCountdown.prototype.changeDateDuration = function() {
	var types = jQuery('.ycd-timer-time-settings');

	if(!types.length) {
		return false;
	}
	var that = this;
	var countdown = this.countdownContainer;
	types.bind('change', function() {
		var val = jQuery(this).val();
		var timeName = jQuery(this).attr('name');
		var options = countdown.data('options');
		options[timeName] = val;

		that.reInitSecondsByOptions(options);
	});
};

YcdSimpleCountdown.prototype.changeFontFamily = function() {
	var types = jQuery('.js-simple-font-family');

	if(!types.length) {
		return false;
	}
	types.bind('change', function() {
		var val = jQuery(this).val();
		var type = jQuery(this).data('field-type');

		jQuery('.ycd-simple-countdown-'+type).css({'font-family': val});
	});
};

YcdSimpleCountdown.prototype.changeFontSizes = function() {
	var types = jQuery('.ycd-simple-font-size');

	if(!types.length) {
		return false;
	}
	var that = this;
	var countdown = this.countdownContainer;
	types.bind('change', function() {
		var val = jQuery(this).val();
		var type = jQuery(this).data('field-type');

		jQuery('.ycd-simple-countdown-'+type).css({'font-size': val});
	});
};

YcdSimpleCountdown.prototype.changeColor = function() {
	var types = jQuery('.js-ycd-simple-time-color');

	if(!types.length) {
		return false;
	}
	var that = this;
	var countdown = this.countdownContainer;
	types.minicolors({
		format: 'rgb',
		opacity: 1,
		change: function () {
			var val = jQuery(this).val();
			var type = jQuery(this).data('time-type');
			jQuery('.ycd-simple-countdown-'+type).css({color: val});
		}
	});
};

YcdSimpleCountdown.prototype.eventListener = function ()
{
	var that = this;

	jQuery(window).bind('ycdChangeDate', function () {
		var val = jQuery('#ycd-date-time-picker').val()+':00';
		var selectedTimezone = jQuery('.js-circle-time-zone option:selected').val();
		var seconds = that.setCounterTime(val, selectedTimezone);
		that.seconds = seconds*1000;
		that.countdown();
	});
};

YcdSimpleCountdown.prototype.changeDateType = function() {
	var types = jQuery('.ycd-date-type');

	if(!types.length) {
		return false;
	}
	var that = this;
	var countdowns = this.countdownContainer;
	types.bind('change', function() {
		var val = jQuery(this).val();
		var timeName = jQuery(this).attr('name');
		var options = countdowns.data('options');
		options[timeName] = val;

		that.reInitSecondsByOptions(options);
	});
};

YcdSimpleCountdown.prototype.reInitSecondsByOptions = function (options)
{
	var seconds = this.getSeconds(options);

	this.seconds = seconds*1000;
	this.countdown();
};


YcdSimpleCountdown.prototype.livePreview = function()
{
	var adminElement = jQuery('.ycd-simple-text');
	if (!adminElement.length) {
		return false;
	}

	this.eventListener();
	this.changeText();
	this.changeSwitch();
	this.changeDateType();
	this.changeDate();
	this.changeTimeZone();
	this.changeDateDuration();
	this.changeFontSizes();
	this.changeFontFamily();
	this.changeColor();
};

YcdSimpleCountdown.prototype.changeText = function()
{
	var texts = jQuery('.ycd-simple-text');

	texts.bind('input', function () {
	   var unite = jQuery(this).data('time-type');
	   jQuery('.ycd-simple-countdown-'+unite+'-label').html(jQuery(this).val());
	});
};

YcdSimpleCountdown.prototype.changeSwitch = function()
{
	var status = jQuery('.js-ycd-time-status');

	if (!status.length) {
		return false;
	}

	status.bind('change', function () {
	   var currentStatus = jQuery(this).is(':checked');
	   var type = jQuery(this).data('time-type');
	   var wrapper = jQuery('.ycd-simple-current-unite-'+type);
	   if (currentStatus) {
		   wrapper.show();
	   }
	   else {
		   wrapper.hide();
	   }
	});
};

YcdSimpleCountdown.prototype.render = function()
{
	this.addTimeToClock();
	this.countdown();
};

YcdSimpleCountdown.prototype.countdown = function()
{
	var unites = ['days', 'hours', 'minutes', 'seconds'];
	var that = this;
	var countdownWrapper = jQuery('.ycd-simple-wrapper-'+this.id);
	var runCountdown = function() {

		// Get today's date and time
		var now = new Date().getTime();

		// Find the distance between now and the count down date
		var distance = that.seconds;

		// Time calculations for days, hours, minutes and seconds
		var unitesValues = {};
		unitesValues.days = Math.floor(distance / (1000 * 60 * 60 * 24));
		unitesValues.hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
		unitesValues.minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
		unitesValues.seconds = Math.floor((distance % (1000 * 60)) / 1000);

		for (var i in unites) {
			var unite = unites[i];
			var selector = '.ycd-simple-countdown-'+unite+'-time';
			jQuery(selector).text(unitesValues[unite]);
		}

		// If the count down is finished, write some text
		if (distance == 0) {
			clearInterval(x);
			that.endBehavior(countdownWrapper, that.options);
		}
		that.seconds -= 1000;
	};
	runCountdown();

	var x = setInterval(function() {
		runCountdown();
	}, 1000);
};

YcdSimpleCountdown.prototype.addTimeToClock = function()
{
	var options = this.options;
	var seconds = this.getSeconds(options);
	this.seconds = seconds*1000;
	this.options['allSeconds'] = seconds;
	this.savedOptions = this.options;
};

jQuery(document).ready(function() {
	YcdSimpleCountdown.run();
});