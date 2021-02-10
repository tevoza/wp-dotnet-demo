<?php
namespace ycd;

class CountdownType {
    private $available = false;
    private $isComingSoon = false;
    private $name = '';
    private $accessLevel = YCD_FREE_VERSION;

    public function setName($name) {
        $this->name = $name;
    }
    public function getName() {
        return $this->name;
    }

    public function setAvailable($available) {
        $this->available = $available;
    }

    public function isAvailable() {
        return $this->available;
    }

    public function setAccessLevel($accessLevel) {
        $this->accessLevel = $accessLevel;
    }

    public function getAccessLevel() {
        return $this->accessLevel;
    }
    
    public function setIsComingSoon($isComingSoon) {
        $this->isComingSoon = $isComingSoon;
    }

    public function getIsComingSoon() {
        return $this->isComingSoon;
    }
}