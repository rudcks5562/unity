package com.ssafy.cookscape.data.model.response;

import com.ssafy.cookscape.data.db.entity.UserAvatarEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class UserAvatarInfo {

    private Long userAvatarId;
    private Long avatarId;
    private String name;
    private String detail;
    private int useCount;

    public static UserAvatarInfo toDto(UserAvatarEntity userAvatar){

        return UserAvatarInfo.builder()
                .userAvatarId(userAvatar.getId())
                .avatarId(userAvatar.getAvatar().getId())
                .name(userAvatar.getAvatar().getName())
                .detail(userAvatar.getAvatar().getDetail())
                .useCount(userAvatar.getUseCount())
                .build();

    }
}
