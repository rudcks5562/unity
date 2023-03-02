package com.ssafy.cookscape.global.common.model.response;

import com.fasterxml.jackson.annotation.JsonFormat;
import lombok.*;

import java.time.ZonedDateTime;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor(access = AccessLevel.PROTECTED)
public class ResponseDto {
	/*
	 * "timestamp": "2023-02-04T07:36:08.031+00:00",
	 * "status": 200,
	 * "message": "No message available",
	 */

	@JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "yyyy-MM-dd'T'HH:mm:ss.SSSXXX")
	private ZonedDateTime timeStamp;

	private int status;

	private String msg;
}