package com.ssafy.cookscape.db.repository;

import com.ssafy.cookscape.db.entity.AvatarEntity;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface AvatarRepository extends JpaRepository<AvatarEntity, Long> {

    Optional<AvatarEntity> findById(Long id);
}
