package com.ssafy.cookscape.global.common.model.response;

import lombok.*;
import org.springframework.http.HttpStatus;

import java.time.ZonedDateTime;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class ResponseErrorDto<T> extends ResponseDto {
	/*
	 * "timestamp": "2023-02-04T07:36:08.031+00:00",
	 * "status": 403,
	 * "message": "No message available",
	 * "error": "Forbidden",
	 * "path": "/"
	 */

	private T error;

	private String path;

	@Builder
	public ResponseErrorDto(ZonedDateTime timeStamp, HttpStatus httpStatus, String path, T error) {
		super(timeStamp, httpStatus.value(), httpStatus.getReasonPhrase());
		this.error = error;
		this.path = path;
	}

}