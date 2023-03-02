package com.ssafy.cookscape.data.model.response;

import com.ssafy.cookscape.information.model.response.AvatarInfo;
import com.ssafy.cookscape.user.model.response.UserInfo;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;

import java.util.List;

@Getter
@Setter
@Builder
public class GameDataResponse {

    private UserInfo userInfo;
    private DataInfo dataInfo;
    private AvatarInfo avatarInfo;
    private List<UserAvatarInfo> userAvatarInfoList;

    public static GameDataResponse makeResponse(
            UserInfo userInfo,
            DataInfo dataInfo,
            AvatarInfo avatarInfo,
            List<UserAvatarInfo> userAvatarInfoList){

        return GameDataResponse.builder()
                .userInfo(userInfo)
                .dataInfo(dataInfo)
                .avatarInfo(avatarInfo)
                .userAvatarInfoList(userAvatarInfoList)
                .build();

    }
}
