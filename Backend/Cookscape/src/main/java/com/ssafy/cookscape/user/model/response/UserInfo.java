package com.ssafy.cookscape.user.model.response;

import com.ssafy.cookscape.user.db.entity.UserEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class UserInfo {

    private Long userId;
    private String nickname;

    public static UserInfo toDto(UserEntity user){
        return UserInfo.builder()
                .userId(user.getId())
                .nickname(user.getNickname())
                .build();

    }
}
