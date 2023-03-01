package com.ssafy.cookscape.global.error.exception;

import com.ssafy.cookscape.global.util.enums.ApiStatus;
import lombok.Getter;

@Getter
public class ApiErrorException extends DefaultErrorException {

	private static final long serialVersionUID = 1L;

	private ApiStatus exception;

	public ApiErrorException(ApiStatus e) {
		super(e.getMessage());
		this.exception = e;
	}

}
