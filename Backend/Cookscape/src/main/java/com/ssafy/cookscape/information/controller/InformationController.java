package com.ssafy.cookscape.information.controller;

import com.ssafy.cookscape.data.service.DataService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Slf4j
@RestController
@RequiredArgsConstructor
@Api(tags = "InformationController v1")
@RequestMapping("/v1/information")
public class InformationController {

    private final DataService dataService;


//    // 게임 기본 데이터 전달
//    @GetMapping("/all")
//    @ApiOperation(value = "게임 기본 데이터 조회")
//    public ResponseEntity<?> getAllInformation(){
//        return ResponseEntity.ok()
//    }

}
