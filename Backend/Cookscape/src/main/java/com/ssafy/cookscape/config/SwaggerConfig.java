package com.ssafy.cookscape.config;

import com.google.common.base.Predicate;
import com.google.common.base.Predicates;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import springfox.documentation.builders.ApiInfoBuilder;
import springfox.documentation.builders.PathSelectors;
import springfox.documentation.builders.RequestHandlerSelectors;
import springfox.documentation.service.ApiInfo;
import springfox.documentation.spi.DocumentationType;
import springfox.documentation.spring.web.plugins.Docket;
import springfox.documentation.swagger2.annotations.EnableSwagger2;

@Configuration
@EnableSwagger2
public class SwaggerConfig {

	private static final String API_NAME = "B109 Project API";
	private static final String API_VERSION = "0.0.1";
	private static final String API_DESCRIPTION = "B109 특화 프로젝트 API 명세서";

	@Bean
	public Docket allApi() {
		String version = "v1";
		return buildDocket("_전체_", Predicates
			.or(PathSelectors.ant("/" + version + "/**")));
	}

	@Bean
	public Docket authApi() {
		String version = "v1";
		return buildDocket("회원 " + version, Predicates
			.or(PathSelectors.ant("/" + version + "/auth/**"),
				PathSelectors.ant("/" + version + "/user"),
				PathSelectors.ant("/" + version + "/user/**")));
	}

	@Bean
	public Docket shelterApi() {
		String version = "v1";
		return buildDocket("보호소 " + version, Predicates
			.or(PathSelectors.ant("/" + version + "/shelter"),
				PathSelectors.ant("/" + version + "/shelter/**")));
	}

	@Bean
	public Docket animalApi() {
		String version = "v1";
		return buildDocket("동물 " + version, Predicates
			.or(PathSelectors.ant("/" + version + "/**/animal"),
				PathSelectors.ant("/" + version + "/**/animal/**")));
	}

	@Bean
	public Docket reservationApi() {
		String version = "v1";
		return buildDocket("예약 " + version, Predicates
			.or(PathSelectors.ant("/" + version + "/schedule/**"),
				PathSelectors.ant("/" + version + "/**/timetable")));
	}

	@Bean
	public Docket alarmApi() {
		String version = "v1";
		return buildDocket("알람 " + version, Predicates
			.or(PathSelectors.ant("/" + version + "/alarm"),
				PathSelectors.ant("/" + version + "/alarm/**")));
	}

	@Bean
	public Docket liveApi() {
		String version = "v1";
		return buildDocket("라이브 " + version, Predicates
			.or(PathSelectors.ant("/" + version + "/live"),
				PathSelectors.ant("/" + version + "/live/**")));
	}

	@Bean
	public Docket otherApi() {
		String version = "v1";
		return buildDocket("기타 " + version, Predicates
			.or(PathSelectors.ant("/error/**"),
				PathSelectors.ant("/" + version + "/file/**"),
				PathSelectors.ant("/" + version + "/openvidu/**")));
	}

	public Docket buildDocket(String groupName, Predicate<String> predicates) {
		return new Docket(DocumentationType.SWAGGER_2)
			.apiInfo(apiInfo()) // API 문서에 대한 설명
			.useDefaultResponseMessages(false)
			.groupName(groupName)
			.select()
			.paths(predicates)
			.apis(RequestHandlerSelectors.any())
			.paths(PathSelectors.any())
			.build();
	}

	public ApiInfo apiInfo() {
		return new ApiInfoBuilder()
			.title(API_NAME)
			.version(API_VERSION)
			.description(API_DESCRIPTION)
			//.license("license")
			//.licenseUrl("license URL")
			.build();
	}

}
