<?php
if(YCD_PKG_VERSION == YCD_FREE_VERSION) {
	$isPro = '-pro';
	$proSpan = '<span class="ycd-pro-span">'.__('pro', YCD_TEXT_DOMAIN).'</span>';
}
?>
<div class="panel panel-default">
	<div class="panel-heading"><?php _e('Options', YCD_TEXT_DOMAIN)?></div>
	<div class="panel-body">
		<div class="row form-group">
			<div class="col-md-6">
				<label for="ycd-coming-soon-whitelist-ip" class="ycd-label-of-switch"><?php _e('White list IP address', YCD_TEXT_DOMAIN); echo $proSpan; ?></label>
			</div>
			<div class="col-md-6 ycd-circles-width-wrapper ycd-option-wrapper<?php echo $isPro; ?>">
				<label class="ycd-switch">
					<input type="checkbox" id="ycd-coming-soon-whitelist-ip" name="ycd-coming-soon-whitelist-ip" class="ycd-accordion-checkbox" <?php echo $this->getOptionValue('ycd-coming-soon-whitelist-ip'); ?>>
					<span class="ycd-slider ycd-round"></span>
				</label>
			</div>
		</div>
		<div class="ycd-accordion-content ycd-hide-content">
			<div class="col-md-3">
				<label for="ycd-coming-soon-ip-address"><?php _e('IP address(s)', YCD_TEXT_DOMAIN);?></label>
			</div>
			<div class="col-md-9">
				<input type="text" class="form-control" name="ycd-coming-soon-ip-address" placeholder="<?php _e('You can enter multiple IP address, just separate them with comma', YCD_TEXT_DOMAIN)?>" value="<?php echo esc_attr($this->getOptionValue('ycd-coming-soon-ip-address'))?>">
			</div>
		</div>
	</div>
</div>