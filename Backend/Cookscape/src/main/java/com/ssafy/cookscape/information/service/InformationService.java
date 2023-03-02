package com.ssafy.cookscape.information.service;

import com.ssafy.cookscape.information.db.repository.AvatarRepository;
import com.ssafy.cookscape.information.db.repository.ItemRepository;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;
import org.springframework.transaction.annotation.Transactional;

@Service
@Transactional(readOnly = true) // 기본적으로 트랜잭션 안에서만 데이터 변경하게 설정(성능 향상)
@RequiredArgsConstructor // Lombok을 사용해 @Autowired 없이 의존성 주입. final 객제만 주입됨을 주의
public class InformationService {

    private final AvatarRepository avatarRepository;
    private final ItemRepository itemRepository;


}
