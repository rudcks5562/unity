package com.ssafy.cookscape.db.repository;

import com.ssafy.cookscape.db.entity.UserDataEntity;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface UserDataRepository extends JpaRepository<UserDataEntity, Long> {

    Optional<UserDataEntity> findById(Long id);
}
