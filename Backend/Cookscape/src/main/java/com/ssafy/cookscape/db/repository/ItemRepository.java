package com.ssafy.cookscape.db.repository;

import com.ssafy.cookscape.db.entity.ItemEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ItemRepository extends JpaRepository<ItemEntity, Long> {
}
