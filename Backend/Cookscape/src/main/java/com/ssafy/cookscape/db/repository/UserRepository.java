package com.ssafy.cookscape.db.repository;

import com.ssafy.cookscape.db.entity.UserEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface UserRepository extends JpaRepository<UserEntity, Long> {
}
