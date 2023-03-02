package com.ssafy.cookscape.data.controller;

import com.ssafy.cookscape.data.service.DataService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.PathVariable;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

import javax.servlet.http.HttpServletRequest;

@Slf4j
@RestController
@RequiredArgsConstructor
@Api(tags = "DataController v1")
@RequestMapping("/v1/data")
public class DataController {

    private final DataService dataService;

    @GetMapping("/all/{userId}")
    @ApiOperation(value = "모든 유저 데이터 조회")
    public ResponseEntity<?> getAllData(@PathVariable Long userId,  HttpServletRequest request){

        return ResponseEntity.ok(dataService.getUserData(userId));
    }
}
