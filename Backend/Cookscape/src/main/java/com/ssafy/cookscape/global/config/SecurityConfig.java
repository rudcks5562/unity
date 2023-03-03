package com.ssafy.cookscape.global.config;

//import com.ssafy.cookscape.global.auth.JwtRequestFilter;
//import com.ssafy.cookscape.global.auth.exception.CustomAccessDeniedHandler;
//import com.ssafy.cookscape.global.auth.exception.CustomAuthenticationEntryPoint;
//import com.ssafy.cookscape.global.util.enums.Role;
import lombok.RequiredArgsConstructor;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.http.HttpMethod;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.config.annotation.authentication.configuration.AuthenticationConfiguration;
import org.springframework.security.config.annotation.web.builders.HttpSecurity;
import org.springframework.security.config.annotation.web.configuration.EnableWebSecurity;
import org.springframework.security.config.annotation.web.configuration.WebSecurityCustomizer;
import org.springframework.security.config.http.SessionCreationPolicy;
import org.springframework.security.crypto.factory.PasswordEncoderFactories;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.security.web.SecurityFilterChain;

@Configuration
@EnableWebSecurity
@RequiredArgsConstructor
public class SecurityConfig {

//	private final JwtRequestFilter jwtRequestFilter;
//
//	private final CustomAccessDeniedHandler customAccessDeniedHandler;
//	private final CustomAuthenticationEntryPoint customAuthenticationEntryPoint;

	@Bean
	public SecurityFilterChain filterChain(HttpSecurity http) throws Exception {
		http.csrf().disable();
		http.sessionManagement().sessionCreationPolicy(SessionCreationPolicy.STATELESS)
			.and()
			.httpBasic()
			.and()
			.exceptionHandling()
//			.accessDeniedHandler(customAccessDeniedHandler)
//			.authenticationEntryPoint(customAuthenticationEntryPoint)
			.and()
			.authorizeRequests()
			.antMatchers(HttpMethod.OPTIONS, "/**/*").permitAll()
			.antMatchers("/").permitAll() // swagger csrf 엔드포인트 오류를 지우기 위함 1
			.antMatchers("/csrf").permitAll() // swagger csrf 엔드포인트 오류를 지우기 위함 2
			.antMatchers("/error/*").permitAll()
			.antMatchers("/*/user", "/*/user/**").permitAll()
			.antMatchers("/*/data", "/*/data/**").permitAll()
			.antMatchers("/*/information", "/*/information/**").permitAll()
			.anyRequest().authenticated();

//		http.addFilterBefore(jwtRequestFilter, UsernamePasswordAuthenticationFilter.class);

		return http.build();
	}

	@Bean
	public WebSecurityCustomizer webSecurityCustomizer() {
		return (web) -> web.ignoring().antMatchers(
			"/v2/api-docs",
			"/swagger-resources/**",
			"/swagger-ui.html",
			"/webjars/**",
			"/swagger/**");
	}

	@Bean
	public AuthenticationManager authenticationManager(AuthenticationConfiguration authenticationConfiguration)
		throws Exception {
		return authenticationConfiguration.getAuthenticationManager();
	}

	@Bean
	public PasswordEncoder passwordEncoder() {
		return PasswordEncoderFactories.createDelegatingPasswordEncoder();
	}

}