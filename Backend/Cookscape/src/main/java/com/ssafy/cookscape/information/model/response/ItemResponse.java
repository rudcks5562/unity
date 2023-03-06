package com.ssafy.cookscape.information.model.response;

import com.ssafy.cookscape.information.db.entity.ItemEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class ItemResponse {

    private Long itemId;
    private String name;
    private String detail;
    private int weight;

    public static ItemResponse toDto(ItemEntity item) {

        return ItemResponse.builder()
                .itemId(item.getId())
                .name(item.getName())
                .detail(item.getDetail())
                .weight(item.getWeight())
                .build();

    }
}
