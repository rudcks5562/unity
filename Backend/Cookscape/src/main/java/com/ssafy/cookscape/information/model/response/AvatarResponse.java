package com.ssafy.cookscape.information.model.response;

import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class AvatarResponse {

    private Long avatarId;
    private String name;
    private String detail;
    private float speed;
    private float jump;

    public static AvatarResponse toDto(AvatarEntity avatar){

        return AvatarResponse.builder()
                .avatarId(avatar.getId())
                .name(avatar.getName())
                .detail(avatar.getDetail())
                .speed(avatar.getSpeed())
                .jump(avatar.getJump())
                .build();

    }
}
