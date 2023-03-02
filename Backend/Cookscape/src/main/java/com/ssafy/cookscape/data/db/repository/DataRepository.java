package com.ssafy.cookscape.data.db.repository;

import com.ssafy.cookscape.data.db.entity.DataEntity;
import org.springframework.data.jpa.repository.JpaRepository;

import java.util.Optional;

public interface DataRepository extends JpaRepository<DataEntity, Long> {

    Optional<DataEntity> findById(Long id);
}
