package com.ssafy.cookscape.service;

import com.ssafy.cookscape.db.entity.AvatarEntity;
import com.ssafy.cookscape.db.entity.UserDataEntity;
import com.ssafy.cookscape.db.entity.UserEntity;
import com.ssafy.cookscape.db.repository.AvatarRepository;
import com.ssafy.cookscape.db.repository.UserDataRepository;
import com.ssafy.cookscape.db.repository.UserRepository;
import com.ssafy.cookscape.global.error.exception.ApiErrorException;
import com.ssafy.cookscape.global.util.enums.ApiStatus;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

import com.ssafy.cookscape.global.model.response.ResponseSuccessDto;
import com.ssafy.cookscape.global.util.ResponseUtil;
import com.ssafy.cookscape.model.request.UserCreateRequest;

import lombok.RequiredArgsConstructor;

@Service
@Transactional(readOnly = true) // 기본적으로 트랜잭션 안에서만 데이터 변경하게 설정(성능 향상)
@RequiredArgsConstructor // Lombok을 사용해 @Autowired 없이 의존성 주입. final 객제만 주입됨을 주의
public class UserService {

    private final PasswordEncoder passwordEncoder;

    private final ResponseUtil responseUtil;

    private final UserRepository userRepository;
    private final UserDataRepository userDataRepository;
    private final AvatarRepository avatarRepository;


    /*
        회원 가입시 userData, userAvatar 정보도 포함해야 한다.
     */
    @Transactional
    public ResponseSuccessDto<?> register(UserCreateRequest userCreateRequest, Long userDataId){

        userCreateRequest.setPassword(passwordEncoder.encode(userCreateRequest.getPassword()));

        UserDataEntity findUserData = userDataRepository.findById(userDataId)
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        AvatarEntity findAvatar = avatarRepository.findById(1L)
                .orElseThrow(() -> new ApiErrorException(ApiStatus.RESOURCE_NOT_FOUND));

        UserEntity user = userCreateRequest.toEntity(findUserData, findAvatar);

        return responseUtil.buildSuccessResponse(userRepository.save(user).getId());
    }
}
