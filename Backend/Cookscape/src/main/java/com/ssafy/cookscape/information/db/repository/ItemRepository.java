package com.ssafy.cookscape.information.db.repository;

import com.ssafy.cookscape.information.db.entity.ItemEntity;
import org.springframework.data.jpa.repository.JpaRepository;

public interface ItemRepository extends JpaRepository<ItemEntity, Long> {
}
