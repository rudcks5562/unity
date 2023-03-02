package com.ssafy.cookscape.user.service;

import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import com.ssafy.cookscape.data.db.entity.DataEntity;
import com.ssafy.cookscape.user.db.entity.UserEntity;
import com.ssafy.cookscape.information.db.repository.AvatarRepository;
import com.ssafy.cookscape.data.db.repository.DataRepository;
import com.ssafy.cookscape.user.db.repository.UserRepository;
import com.ssafy.cookscape.global.error.exception.ApiErrorException;
import com.ssafy.cookscape.global.util.enums.ApiStatus;
import com.ssafy.cookscape.user.model.request.SignInRequest;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.ssafy.cookscape.global.common.model.response.ResponseSuccessDto;
import com.ssafy.cookscape.global.util.ResponseUtil;
import com.ssafy.cookscape.user.model.request.SignUpRequest;

import lombok.RequiredArgsConstructor;

@Service
@Transactional(readOnly = true) // 기본적으로 트랜잭션 안에서만 데이터 변경하게 설정(성능 향상)
@RequiredArgsConstructor // Lombok을 사용해 @Autowired 없이 의존성 주입. final 객제만 주입됨을 주의
public class UserService {

    private final PasswordEncoder passwordEncoder;

    private final ResponseUtil responseUtil;

    private final UserRepository userRepository;
    private final DataRepository dataRepository;
    private final AvatarRepository avatarRepository;


    /*
        회원 가입시 userData, userAvatar 정보도 포함해야 한다.
     */
    @Transactional
    public Long signUp(SignUpRequest userDto, Long userDataId){

        userDto.setPassword(passwordEncoder.encode(userDto.getPassword()));

        DataEntity findUserData = dataRepository.findById(userDataId)
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        AvatarEntity findAvatar = avatarRepository.findById(1L)
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        UserEntity user = userDto.toEntity(findUserData, findAvatar);

        return userRepository.save(user).getId();
    }

    @Transactional
    public ResponseSuccessDto<?> signIn(SignInRequest signInDto){

        UserEntity findUser = userRepository.findByLoginIdAndExpiredLike(signInDto.getLoginId(), "F")
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        if(!passwordEncoder.matches(signInDto.getPassword(), findUser.getPassword())){
            throw new ApiErrorException(ApiStatus.INVALID_PASSWORD);
        }

        return responseUtil.buildSuccessResponse(findUser.getId());

    }

    @Transactional
    public ResponseSuccessDto<?> checkId(String loginId){

        if(null != userRepository.findByLoginId(loginId)){
            throw new ApiErrorException(ApiStatus.ID_DUPLICATION);
        }

        return responseUtil.buildSuccessResponse(loginId);
    }

    @Transactional
    public ResponseSuccessDto<?> checkNickname(String nickname){

        if(null != userRepository.findByNickname(nickname)){
            throw new ApiErrorException(ApiStatus.NICKNAME_DUPLICATION);
        }

        return responseUtil.buildSuccessResponse(nickname);
    }

}
