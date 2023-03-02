package com.ssafy.cookscape.global.common.service;

import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import com.ssafy.cookscape.information.db.repository.AvatarRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import javax.transaction.Transactional;

@Service
@RequiredArgsConstructor
public class initialData {

    private final AvatarRepository avatarRepository;
    @Transactional
    @PostConstruct
    public void initialData(){

        AvatarEntity avatar = AvatarEntity.builder()
                .name("요리사")
                .detail("기본 술래 캐릭터입니다.")
                .speed(1.0f)
                .jump(1.0f)
                .build();

        avatarRepository.save(avatar);

        AvatarEntity avatar1 = AvatarEntity.builder()
                .name("햄버거")
                .detail("기본 유저 캐릭터입니다.")
                .speed(1.0f)
                .jump(1.0f)
                .build();

        avatarRepository.save(avatar1);
    }
}
