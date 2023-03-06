package com.ssafy.cookscape.user.model.request;

import lombok.Getter;

@Getter
public class SignInRequest {

    private String loginId;
    private String password;

}
