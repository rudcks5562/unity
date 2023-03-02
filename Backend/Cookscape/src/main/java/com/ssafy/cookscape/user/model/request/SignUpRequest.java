package com.ssafy.cookscape.user.model.request;

import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import com.ssafy.cookscape.data.db.entity.DataEntity;
import com.ssafy.cookscape.user.db.entity.UserEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

@Getter
@Setter
@Builder
public class SignUpRequest {

    private String loginId;
    private String password;
    private String nickname;

    public UserEntity toEntity(DataEntity data, AvatarEntity avatar){
        return UserEntity.builder()
                .data(data)
                .avatar(avatar)
                .loginId(this.loginId)
                .password(this.password)
                .nickname(this.nickname)
                .build();
    }
}
