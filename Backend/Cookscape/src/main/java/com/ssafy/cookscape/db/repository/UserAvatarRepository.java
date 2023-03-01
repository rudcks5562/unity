package com.ssafy.cookscape.db.repository;

import com.ssafy.cookscape.db.entity.UserAvatarEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserAvatarRepository extends JpaRepository<UserAvatarEntity, Long> {
}
