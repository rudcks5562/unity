package com.ssafy.cookscape.model.request;

import com.ssafy.cookscape.db.entity.AvatarEntity;
import com.ssafy.cookscape.db.entity.UserDataEntity;
import com.ssafy.cookscape.db.entity.UserEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class UserCreateRequest {

    private String loginId;
    private String password;
    private String nickname;

    public UserEntity toEntity(UserDataEntity userData, AvatarEntity avatar){
        return UserEntity.builder()
                .userData(userData)
                .avatar(avatar)
                .loginId(this.loginId)
                .password(this.password)
                .nickname(this.nickname)
                .build();
    }
}
