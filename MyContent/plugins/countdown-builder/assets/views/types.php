<?php
use ycd\Countdown;
use ycd\AdminHelper;
$types = Countdown::getCountdownTypes();
$currentExtensions = YcdCountdownConfig::extensions();
$extensionsResult = AdminHelper::separateToActiveAndNotActive($currentExtensions);
?>
<div class="ycd-bootstrap-wrapper ycd-types-wrapper">
	<div class="row">
		<div class="col-md-12">
			<h3><?php _e('Add New Countdown', YCD_TEXT_DOMAIN); ?></h3>
		</div>
	</div>
    <div class="add-new-hr"></div>
	<?php foreach ($types as $type): ?>
        <?php if(YCD_PKG_VERSION > YCD_FREE_VERSION && !$type->isAvailable()): ?>
            <?php continue; ?>
        <?php endif; ?>
		<a class="create-countdown-link" <?php echo AdminHelper::buildCreateCountdownAttrs($type); ?> href="<?php echo AdminHelper::buildCreateCountdownUrl($type); ?>">
            <div class="countdowns-div">
                <div class="ycd-type-div <?php echo AdminHelper::getCountdownThumbClass($type); ?>"></div>
                <?php echo AdminHelper::getCountdownThumbText($type); ?>
                <div class="ycd-type-view-footer">
                    <span class="ycd-promotion-video"><?php echo AdminHelper::getCountdownDisplayName($type); ?></span>
                    <?php
                        $videoUrl = AdminHelper::getCountdownYoutubeUrl($type);
                        if(!$type->isAvailable() && !empty($videoUrl)): ?>
                        <span class="ycd-play-promotion-video" data-href="<?php echo $videoUrl; ?>"></span>
                    <?php endif; ?>
                </div> 
            </div>
        </a>
	<?php endforeach; ?>
    <div class="clear"></div>
    <?php if(!empty($extensionsResult['passive'])) : ?>
    <div class="ycd-add-new-extensions-wrapper">
        <span class="ycd-add-new-extensions">
            Extensions
        </span>
    </div>
        <?php foreach ($extensionsResult['passive'] as $extension): ?>
            <a class="create-countdown-link" <?php echo AdminHelper::buildCreateCountdownAttrs($type); ?> href="<?php echo YCD_COUNTDOWN_PRO_URL.'#yrm-analytics'; ?>">
                <div class="countdowns-div">
                    <div class="ycd-type-div <?php echo $extension['shortKey']?>-countdown-pro ycd-pro-version"></div>
                    <p class="ycd-type-title-pro ycd-type-title-extensions-pro"><?php echo __('PRO EXTENSION', YCD_TEXT_DOMAIN); ?></p>
                    <div class="ycd-type-view-footer">
                        <span class="ycd-promotion-video"><?php echo $extension['boxTitle']; ?></span>
                        <?php if(!empty($extension['videoURL'])): ?>
                            <span class="ycd-play-promotion-video" data-href="<?php echo esc_attr($extension['videoURL']); ?>"></span>
                        <?php endif; ?>
                    </div>
                </div>
            </a>
        <?php endforeach; ?>
    <?php endif; ?>
</div>