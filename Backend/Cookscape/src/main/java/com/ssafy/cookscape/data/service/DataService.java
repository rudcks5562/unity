package com.ssafy.cookscape.data.service;

import com.ssafy.cookscape.data.db.entity.DataEntity;
import com.ssafy.cookscape.data.db.entity.UserAvatarEntity;
import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import com.ssafy.cookscape.information.db.repository.AvatarRepository;
import com.ssafy.cookscape.user.db.entity.UserEntity;
import com.ssafy.cookscape.data.db.repository.DataRepository;
import com.ssafy.cookscape.data.db.repository.UserAvatarRepository;
import com.ssafy.cookscape.user.db.repository.UserRepository;
import com.ssafy.cookscape.global.error.exception.ApiErrorException;
import com.ssafy.cookscape.global.common.model.response.ResponseSuccessDto;
import com.ssafy.cookscape.global.util.ResponseUtil;
import com.ssafy.cookscape.global.util.enums.ApiStatus;
import com.ssafy.cookscape.information.model.response.AvatarInfo;
import com.ssafy.cookscape.data.model.response.DataInfo;
import com.ssafy.cookscape.data.model.response.UserAvatarInfo;
import com.ssafy.cookscape.user.model.response.UserInfo;
import com.ssafy.cookscape.data.model.response.GameDataResponse;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import javax.servlet.http.HttpServletRequest;
import java.util.ArrayList;
import java.util.List;
import java.util.stream.Collectors;

@Service
@Transactional(readOnly = true) // 기본적으로 트랜잭션 안에서만 데이터 변경하게 설정(성능 향상)
@RequiredArgsConstructor // Lombok을 사용해 @Autowired 없이 의존성 주입. final 객제만 주입됨을 주의
public class DataService {

    private final ResponseUtil responseUtil;

    private final DataRepository dataRepository;
    private final UserRepository userRepository;
    private final AvatarRepository avatarRepository;
    private final UserAvatarRepository userAvatarRepository;

    @Transactional
    public Long addUserData(){

        DataEntity saveUserData = DataEntity.builder().level(1).build();

        return dataRepository.save(saveUserData).getId();
    }

    @Transactional
    public ResponseSuccessDto<?> addUserAvatarData(Long userId){

        UserEntity findUser = userRepository.findById(userId)
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        List<AvatarEntity> findAvatarList = avatarRepository.findAll();

        List<UserAvatarEntity> saveUserAvatarList = new ArrayList<>();

        for(AvatarEntity avatar : findAvatarList){
            saveUserAvatarList.add(UserAvatarEntity.builder()
                    .avatar(avatar)
                    .user(findUser)
                    .build());
        }

        userAvatarRepository.saveAll(saveUserAvatarList);

        return responseUtil.buildSuccessResponse(null);

    }

    @Transactional
    public ResponseSuccessDto<?> getUserData(Long userId){

        UserEntity findUser = userRepository.findByIdAndExpiredLike(userId, "F")
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        //userInfo - dataInfo - avatarInfo - userAvatarInfo - GameDataResponse

        UserInfo userInfo = UserInfo.toDto(findUser);
        DataInfo dataInfo = DataInfo.toDto(findUser.getData());
        AvatarInfo avatarInfo = AvatarInfo.toDto(findUser.getAvatar());

        List<UserAvatarEntity> findUserAvatarList = userAvatarRepository.findByUser(findUser);

        List<UserAvatarInfo> userAvatarInfoList = findUserAvatarList.stream()
                .map(UserAvatarInfo::toDto)
                .collect(Collectors.toList());

        return responseUtil.buildSuccessResponse(GameDataResponse.makeResponse(userInfo, dataInfo, avatarInfo, userAvatarInfoList));


    }
}
