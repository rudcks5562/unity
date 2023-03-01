package com.ssafy.cookscape.controller;

import com.ssafy.cookscape.model.request.UserCreateRequest;
import com.ssafy.cookscape.service.UserDataService;
import com.ssafy.cookscape.service.UserService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@Slf4j
@RestController
@RequiredArgsConstructor
@Api(tags = "UserController v1")
@RequestMapping("/v1/user")
public class UserController {

    private final UserService userService;
    private final UserDataService userDataService;

    // 회원가입
    // 회원가입 성공 시 유저에 대한 모든 기본 유저-데이터, 유저-아바타 테이블 생성
    @PostMapping
    @ApiOperation(value = "회원 가입")
    public ResponseEntity<?> signUp(@RequestBody UserCreateRequest userCreateRequest){
        Long userDataId = userDataService.register();
        return ResponseEntity.ok(userService.register(userCreateRequest, userDataId));
    }

    // 로그인
    // 로그인 성공 시, 유저-데이터, 유저-아바타 정보 전달




}
