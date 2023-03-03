package com.ssafy.cookscape.global.common.service;

import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import com.ssafy.cookscape.information.db.entity.ItemEntity;
import com.ssafy.cookscape.information.db.repository.AvatarRepository;
import com.ssafy.cookscape.information.db.repository.ItemRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import javax.annotation.PostConstruct;
import javax.transaction.Transactional;

@Service
@RequiredArgsConstructor
public class initialData {

    private final ItemRepository itemRepository;
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

        ItemEntity item = ItemEntity.builder()
                .name("나무젓가락")
                .detail("이거 정말 무겁습니다!")
                .weight(5)
                .build();

        itemRepository.save(item);

        ItemEntity item1 = ItemEntity.builder()
                .name("키친타월")
                .detail("'어떻게 이런 보습력이 존재할 수 있는거지?' - 이지우")
                .weight(5)
                .build();

        itemRepository.save(item1);
    }
}
