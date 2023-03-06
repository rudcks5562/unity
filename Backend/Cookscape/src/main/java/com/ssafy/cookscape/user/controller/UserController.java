package com.ssafy.cookscape.user.controller;

import com.ssafy.cookscape.user.model.request.SignInRequest;
import com.ssafy.cookscape.user.model.request.SignUpRequest;
import com.ssafy.cookscape.data.service.DataService;
import com.ssafy.cookscape.user.service.UserService;
import io.swagger.annotations.Api;
import io.swagger.annotations.ApiOperation;
import lombok.RequiredArgsConstructor;
import lombok.extern.slf4j.Slf4j;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.*;

@Slf4j
@RestController
@RequiredArgsConstructor
@Api(tags = "UserController v1")
@RequestMapping("/v1/user")
public class UserController {

    private final UserService userService;
    private final DataService dataService;

    // 회원가입
    // 회원가입 성공 시 유저에 대한 모든 기본 유저-데이터, 유저-아바타 테이블 생성
    @PostMapping("/signup")
    @ApiOperation(value = "회원 가입")
    public ResponseEntity<?> signUp(@RequestBody SignUpRequest signUpDto){
        Long dataId = dataService.addUserData();
        Long userId = userService.signUp(signUpDto, dataId);
        return ResponseEntity.ok(dataService.addUserAvatarData(userId));
    }

    // 로그인
    @PostMapping("/signin")
    @ApiOperation(value = "로그인")
    public ResponseEntity<?> signIn(@RequestBody SignInRequest signInDto){
        return ResponseEntity.ok(userService.signIn(signInDto));
    }

    // 아이디 중복체크
    @GetMapping("/id-check/{loginId}")
    @ApiOperation(value = "아이디 중복 체크")
    public ResponseEntity<?> checkId(@PathVariable String loginId){
        return ResponseEntity.ok(userService.checkId(loginId));
    }

    // 닉네임 중복체크
    @GetMapping("/nickname-check/{nickname}")
    @ApiOperation(value = "닉네임 중복 체크")
    public ResponseEntity<?> checkNickname(@PathVariable String nickname){
        return ResponseEntity.ok(userService.checkNickname(nickname));
    }

}
