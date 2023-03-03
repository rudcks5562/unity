package com.ssafy.cookscape.data.model.request;

import com.ssafy.cookscape.data.db.entity.DataEntity;
import lombok.Builder;
import lombok.Getter;
import lombok.Setter;
import lombok.ToString;

@Getter
@Setter
@Builder
@ToString
public class GameResultRequest {

    private Long userId;
    private int exp;
    private int level;
    private int money;
    private boolean isWin;
    private int saveCount;
    private int catchCount;
    private int catchedCount;
    private int valveOpenCount;
    private int valveCloseCount;
    private int potDestroyCount;

    public DataEntity toEntity(DataEntity data){

        return DataEntity.builder()
                .id(data.getId())
                .user(data.getUser())
                .exp(data.getExp() + this.getExp())
                .level(this.getLevel())
                .money(data.getMoney() + this.getMoney())
                .winCount(isWin ? data.getWinCount() + 1 : data.getWinCount())
                .loseCount(isWin ? data.getLoseCount() : data.getLoseCount() + 1)
                .saveCount(data.getSaveCount() + this.getSaveCount())
                .catchCount(data.getCatchCount() + this.getCatchCount())
                .catchedCount(data.getCatchedCount() + this.getCatchedCount())
                .valveOpenCount(data.getValveOpenCount() + this.getValveOpenCount())
                .valveCloseCount(data.getValveCloseCount() + this.getValveCloseCount())
                .potDestroyCount(data.getPotDestroyCount() + this.getPotDestroyCount())
                .expired(data.getExpired())
                .build();

    }
}
