package com.ssafy.cookscape.data.db.entity;

import com.ssafy.cookscape.global.common.db.entity.BaseEntity;
import com.ssafy.cookscape.user.db.entity.UserEntity;
import lombok.AccessLevel;
import lombok.Getter;
import lombok.NoArgsConstructor;
import lombok.ToString;
import lombok.experimental.SuperBuilder;
import org.hibernate.annotations.ColumnDefault;
import org.hibernate.annotations.DynamicInsert;
import org.hibernate.annotations.Fetch;

import javax.persistence.*;

@Entity
@DynamicInsert
@Table(name = "data")
@Getter
@SuperBuilder
@NoArgsConstructor(access = AccessLevel.PROTECTED)
@ToString
public class DataEntity extends BaseEntity {

    private static final long serialVersionUID = 1L;

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    @Column(name = "id", columnDefinition = "INT UNSIGNED")
    private Long id;

    @OneToOne(mappedBy = "data", fetch = FetchType.LAZY, cascade = CascadeType.ALL)
    private UserEntity user;

    @Column(name = "exp")
    @ColumnDefault("0")
    private int exp;

    @Column(name = "level")
    @ColumnDefault("1")
    private int level;

    @Column(name = "money")
    @ColumnDefault("0")
    private int money;

    @Column(name = "win_count")
    @ColumnDefault("0")
    private int winCount;

    @Column(name = "lose_count")
    @ColumnDefault("0")
    private int loseCount;

    @Column(name = "save_count")
    @ColumnDefault("0")
    private int saveCount;

    @Column(name = "catch_count")
    @ColumnDefault("0")
    private int catchCount;

    @Column(name = "catched_count")
    @ColumnDefault("0")
    private int catchedCount;

    @Column(name = "valve_open_count")
    @ColumnDefault("0")
    private int valveOpenCount;

    @Column(name = "valve_close_count")
    @ColumnDefault("0")
    private int valveCloseCount;

    @Column(name = "pot_destroy_count")
    @ColumnDefault("0")
    private int potDestroyCount;


}
