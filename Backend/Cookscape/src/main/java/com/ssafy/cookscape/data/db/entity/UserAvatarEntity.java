package com.ssafy.cookscape.data.db.entity;

import com.ssafy.cookscape.global.common.db.entity.BaseEntity;
import com.ssafy.cookscape.information.db.entity.AvatarEntity;
import com.ssafy.cookscape.user.db.entity.UserEntity;
import lombok.AccessLevel;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.experimental.SuperBuilder;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.DynamicInsert;

import javax.persistence.*;

@Entity
@DynamicInsert
@Table(name = "user_avatar")
@Getter
@SuperBuilder
@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class UserAvatarEntity extends BaseEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", columnDefinition = "INT UNSIGNED")
    private Long id;

    @ManyToOne
    @JoinColumn(name = "user_id")
    private UserEntity user;

    @ManyToOne
    @JoinColumn(name = "avatar_id")
    private AvatarEntity avatar;

    @Column
    @ColumnDefault("0")
    private int useCount;
}
