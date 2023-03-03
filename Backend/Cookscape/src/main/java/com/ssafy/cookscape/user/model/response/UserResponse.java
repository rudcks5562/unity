package com.ssafy.cookscape.user.model.response;

import com.ssafy.cookscape.user.db.entity.UserEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class UserResponse {

    private Long userId;
    private String nickname;
    private Long avatarId;
    private String avatarName;
    private String avatarDetail;
    private float avatarSpeed;
    private float avatarJump;

    public static UserResponse toDto(UserEntity user){
        return UserResponse.builder()
                .userId(user.getId())
                .nickname(user.getNickname())
                .avatarId(user.getAvatar().getId())
                .avatarName(user.getAvatar().getName())
                .avatarDetail(user.getAvatar().getDetail())
                .avatarSpeed(user.getAvatar().getSpeed())
                .avatarJump(user.getAvatar().getJump())
                .build();

    }
}
