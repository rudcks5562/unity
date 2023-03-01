package com.ssafy.cookscape.db.entity;

import com.ssafy.cookscape.db.entity.BaseEntity;
import lombok.AccessLevel;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.experimental.SuperBuilder;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.DynamicInsert;

import javax.persistence.*;

@Entity
@DynamicInsert
@Table(name = "avatar")
@Getter
@SuperBuilder
@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class AvatarEntity extends BaseEntity {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", columnDefinition = "INT UNSIGNED")
    private Long id;

    // 아바타 이름
    @Column(name = "name", nullable = false, unique = true)
    private String name;

    // 아바타 설명
    @Column(name = "detail", length = 1000)
    private String detail;

    // 아바타 속도, 기본속도:1 (수정필요)
    @Column(name = "speed", nullable = false)
    private float speed;

    // 아바타 점프력, 기본점프력:1 (수정필요)
    @Column(name = "jump", nullable = false)
    private float jump;
}
