/* USER CODE BEGIN Header */
/**
  ******************************************************************************
  * @file           : main.c
  * @brief          : Main program body
  ******************************************************************************
  * @attention
  *
  * <h2><center>&copy; Copyright (c) 2020 STMicroelectronics.
  * All rights reserved.</center></h2>
  *
  * This software component is licensed by ST under BSD 3-Clause license,
  * the "License"; You may not use this file except in compliance with the
  * License. You may obtain a copy of the License at:
  *                        opensource.org/licenses/BSD-3-Clause
  *
  ******************************************************************************
  */
/* USER CODE END Header */

/* Includes ------------------------------------------------------------------*/
#include "main.h"
#include "usb_device.h"

/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */
#include "usbd_cdc_if.h"
#include "Math.h"
/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */
#define pi 3.14159265359f
#define T 0.02f
#define in1 2.3f
#define in2 4.6f
#define in3 6.23f
#define in4 15.0f
#define in5 22.0f

//float pid1[3] = {2.2f, 5.1f, 2.75f};
//float pid2[3] = {1.35f, 4.1f, 3.28f};
//float pid3[3] = {0.7f, 3.5f, 3.59f};
//float pid4[3] = {0.57f, 3.5f, 6.1f};
//float pid5[3] = {0.47f, 3.4f, 9.1f};

float pid1[4] = {2.2f, 5.1f, 2.0f, 2.7f};
float pid2[4] = {1.35f, 4.1f, 2.8f, 2.6f};
float pid3[4] = {0.7f, 3.5f, 3.2f, 2.5f};
float pid4[4] = {0.57f, 3.5f, 5.2f, 2.3f};
float pid5[4] = {0.47f, 3.4f, 7.7f, 2.1f};
/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */

/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */

/* Private variables ---------------------------------------------------------*/
TIM_HandleTypeDef htim1;
TIM_HandleTypeDef htim2;
TIM_HandleTypeDef htim3;

/* USER CODE BEGIN PV */

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_TIM1_Init(void);
static void MX_TIM2_Init(void);
static void MX_TIM3_Init(void);
/* USER CODE BEGIN PFP */
void fuzzy(float set_point, float *Kp, float *Ki, float *Kd);
void fuzzy_setpos(float set_point, float *Kp);
/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */
struct package{
	uint8_t header[4];
	float data;
}data_receive;
struct send_package{
	float data;
	float set;
	float voltage;
	float kp;
	float ki;
	float kd;
}data_sp, data_po;
struct Tune{
	float kp;
	float ki;
	float kd;
	//float th;
}tune;
uint16_t pulse, pulse_k1;
int16_t count_pulse, last_pulse, current_pulse;
float speed, set_speed, set_speed_k1, set_speed_k2, kp, ki, kd, u, u_k1, e, e_k1, e_k2, Kpv = 2.3f, Kdv = 0.0005f;
float ev, ev_k1, ev_k2, ev_k3, uv;
float set_pos, pos;
float voltage, th;
uint16_t pwm_out;
uint8_t check = 0;
uint8_t first_start = 0;
uint8_t selection = 0;
uint8_t point;
uint8_t length_check;
uint8_t para_data[30];
uint8_t sp_header[4] = {2, 22, 's', 'p'};
uint8_t po_header[4] = {2, 22, 's', 'p'};
uint8_t ack_header[4] = {2, 22, 6, 6};
uint8_t end_package[2] = {22, 3};
uint8_t send[8];
union real{
	float real;
	uint8_t byte[4];
}convert;
/* USER CODE END 0 */

/**
  * @brief  The application entry point.
  * @retval int
  */
int main(void)
{
  /* USER CODE BEGIN 1 */
  memcpy(&para_data[28], end_package, 2);
  /* USER CODE END 1 */
  
  
  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();
  MX_TIM1_Init();
  MX_TIM2_Init();
  MX_TIM3_Init();
  MX_USB_DEVICE_Init();
  /* USER CODE BEGIN 2 */
	TIM3->ARR = 19999;
	//HAL_TIM_Base_Start_IT(&htim3);
  HAL_TIM_Encoder_Start(&htim1, TIM_CHANNEL_1 | TIM_CHANNEL_2);
	HAL_TIM_PWM_Start(&htim2, TIM_CHANNEL_1);
	HAL_GPIO_WritePin(GPIOC, GPIO_PIN_0, GPIO_PIN_SET);
	HAL_GPIO_WritePin(GPIOC, GPIO_PIN_2, GPIO_PIN_RESET);
  TIM1->SR = 0;
  /* USER CODE END 2 */

  /* Infinite loop */
  /* USER CODE BEGIN WHILE */
  while (1)
  {
		

    /* USER CODE END WHILE */   
    /* USER CODE BEGIN 3 */
  }
  /* USER CODE END 3 */
}

/**
  * @brief System Clock Configuration
  * @retval None
  */
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Configure the main internal regulator output voltage 
  */
  __HAL_RCC_PWR_CLK_ENABLE();
  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE1);
  /** Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSE;
  RCC_OscInitStruct.HSEState = RCC_HSE_ON;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_ON;
  RCC_OscInitStruct.PLL.PLLSource = RCC_PLLSOURCE_HSE;
  RCC_OscInitStruct.PLL.PLLM = 4;
  RCC_OscInitStruct.PLL.PLLN = 168;
  RCC_OscInitStruct.PLL.PLLP = RCC_PLLP_DIV2;
  RCC_OscInitStruct.PLL.PLLQ = 7;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }
  /** Initializes the CPU, AHB and APB busses clocks 
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_PLLCLK;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV4;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV2;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_5) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief TIM1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM1_Init(void)
{

  /* USER CODE BEGIN TIM1_Init 0 */

  /* USER CODE END TIM1_Init 0 */

  TIM_Encoder_InitTypeDef sConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM1_Init 1 */

  /* USER CODE END TIM1_Init 1 */
  htim1.Instance = TIM1;
  htim1.Init.Prescaler = 0;
  htim1.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim1.Init.Period = 0xffff;
  htim1.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim1.Init.RepetitionCounter = 0;
  htim1.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  sConfig.EncoderMode = TIM_ENCODERMODE_TI12;
  sConfig.IC1Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC1Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC1Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC1Filter = 0;
  sConfig.IC2Polarity = TIM_ICPOLARITY_RISING;
  sConfig.IC2Selection = TIM_ICSELECTION_DIRECTTI;
  sConfig.IC2Prescaler = TIM_ICPSC_DIV1;
  sConfig.IC2Filter = 0;
  if (HAL_TIM_Encoder_Init(&htim1, &sConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim1, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM1_Init 2 */

  /* USER CODE END TIM1_Init 2 */

}

/**
  * @brief TIM2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM2_Init(void)
{

  /* USER CODE BEGIN TIM2_Init 0 */

  /* USER CODE END TIM2_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};
  TIM_OC_InitTypeDef sConfigOC = {0};

  /* USER CODE BEGIN TIM2_Init 1 */

  /* USER CODE END TIM2_Init 1 */
  htim2.Instance = TIM2;
  htim2.Init.Prescaler = 21;
  htim2.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim2.Init.Period = 1023;
  htim2.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim2.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim2, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  if (HAL_TIM_PWM_Init(&htim2) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_RESET;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim2, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sConfigOC.OCMode = TIM_OCMODE_PWM1;
  sConfigOC.Pulse = 0;
  sConfigOC.OCPolarity = TIM_OCPOLARITY_HIGH;
  sConfigOC.OCFastMode = TIM_OCFAST_DISABLE;
  if (HAL_TIM_PWM_ConfigChannel(&htim2, &sConfigOC, TIM_CHANNEL_1) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM2_Init 2 */

  /* USER CODE END TIM2_Init 2 */
  HAL_TIM_MspPostInit(&htim2);

}

/**
  * @brief TIM3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_TIM3_Init(void)
{

  /* USER CODE BEGIN TIM3_Init 0 */

  /* USER CODE END TIM3_Init 0 */

  TIM_ClockConfigTypeDef sClockSourceConfig = {0};
  TIM_MasterConfigTypeDef sMasterConfig = {0};

  /* USER CODE BEGIN TIM3_Init 1 */

  /* USER CODE END TIM3_Init 1 */
  htim3.Instance = TIM3;
  htim3.Init.Prescaler = 83;
  htim3.Init.CounterMode = TIM_COUNTERMODE_UP;
  htim3.Init.Period = 0;
  htim3.Init.ClockDivision = TIM_CLOCKDIVISION_DIV1;
  htim3.Init.AutoReloadPreload = TIM_AUTORELOAD_PRELOAD_DISABLE;
  if (HAL_TIM_Base_Init(&htim3) != HAL_OK)
  {
    Error_Handler();
  }
  sClockSourceConfig.ClockSource = TIM_CLOCKSOURCE_INTERNAL;
  if (HAL_TIM_ConfigClockSource(&htim3, &sClockSourceConfig) != HAL_OK)
  {
    Error_Handler();
  }
  sMasterConfig.MasterOutputTrigger = TIM_TRGO_UPDATE;
  sMasterConfig.MasterSlaveMode = TIM_MASTERSLAVEMODE_DISABLE;
  if (HAL_TIMEx_MasterConfigSynchronization(&htim3, &sMasterConfig) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN TIM3_Init 2 */

  /* USER CODE END TIM3_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOH_CLK_ENABLE();
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOE_CLK_ENABLE();
  __HAL_RCC_GPIOD_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOC, GPIO_PIN_0|GPIO_PIN_2, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOD, GPIO_PIN_12, GPIO_PIN_RESET);

  /*Configure GPIO pins : PC0 PC2 */
  GPIO_InitStruct.Pin = GPIO_PIN_0|GPIO_PIN_2;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOC, &GPIO_InitStruct);

  /*Configure GPIO pin : PA0 */
  GPIO_InitStruct.Pin = GPIO_PIN_0;
  GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
  GPIO_InitStruct.Pull = GPIO_PULLDOWN;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pin : PD12 */
  GPIO_InitStruct.Pin = GPIO_PIN_12;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOD, &GPIO_InitStruct);

}

/* USER CODE BEGIN 4 */
void fuzzy(float set_point, float *Kp, float *Ki, float *Kd)
{
	float a1, a2;
  if (set_point >= 0 && set_point < in1) {
    *Kp = pid1[0]*set_point/in1 ;*Ki = pid1[1]*set_point/in1; *Kd = pid1[2]*set_point/in1 ;
	}else if(set_point >= in1 && set_point < in2){
		a1 = (in2 - set_point) / (in2 - in1);
		a2 = (set_point - in1) / (in2 - in1);
		*Kp = (pid1[0]*a1 + pid2[0]*a2) / (a1 + a2);
		*Ki = (pid1[1]*a1 + pid2[1]*a2) / (a1 + a2);
		*Kd = (pid1[2]*a1 + pid2[2]*a2) / (a1 + a2);
	}else if(set_point >= in2 && set_point < in3){
		a1 = (in3 - set_point) / (in3 - in2);
		a2 = (set_point - in2) / (in3 - in2);
		*Kp = (pid2[0]*a1 + pid3[0]*a2) / (a1 + a2);
		*Ki = (pid2[1]*a1 + pid3[1]*a2) / (a1 + a2);
		*Kd = (pid2[2]*a1 + pid3[2]*a2) / (a1 + a2);
	}else if(set_point >= in3 && set_point < in4){
		a1 = (in4 - set_point) / (in4 - in3);
		a2 = (set_point - in3) / (in4 - in3);
		*Kp = (pid3[0]*a1 + pid4[0]*a2) / (a1 + a2);
		*Ki = (pid3[1]*a1 + pid4[1]*a2) / (a1 + a2);
		*Kd = (pid3[2]*a1 + pid4[2]*a2) / (a1 + a2);
	}else if(set_point >= in4 && set_point < in5){
		a1 = (in5 - set_point) / (in5 - in4);
		a2 = (set_point - in4) / (in5 - in4);
		*Kp = (pid4[0]*a1 + pid5[0]*a2) / (a1 + a2);
		*Ki = (pid4[1]*a1 + pid5[1]*a2) / (a1 + a2);
		*Kd = (pid4[2]*a1 + pid5[2]*a2) / (a1 + a2);
	}else if(set_point >= in5){
		*Kp = pid5[0];*Ki = pid5[1];*Kd = pid5[2];
	}		
}
void fuzzy_setpos(float set_point, float *Kp)
{
	float a1, a2;
  if (set_point >= 0 && set_point < in1) {
    *Kp = pid1[3];
	}else if(set_point >= in1 && set_point < in2){
		a1 = (in2 - set_point) / (in2 - in1);
		a2 = (set_point - in1) / (in2 - in1);
		*Kp = (pid1[3]*a1 + pid2[3]*a2) / (a1 + a2);
	}else if(set_point >= in2 && set_point < in3){
		a1 = (in3 - set_point) / (in3 - in2);
		a2 = (set_point - in2) / (in3 - in2);
		*Kp = (pid2[2]*a1 + pid3[3]*a2) / (a1 + a2);
	}else if(set_point >= in3 && set_point < in4){
		a1 = (in4 - set_point) / (in4 - in3);
		a2 = (set_point - in3) / (in4 - in3);
		*Kp = (pid3[2]*a1 + pid4[3]*a2) / (a1 + a2);
	}else if(set_point >= in4 && set_point < in5){
		a1 = (in5 - set_point) / (in5 - in4);
		a2 = (set_point - in4) / (in5 - in4);
		*Kp = (pid4[3]*a1 + pid5[3]*a2) / (a1 + a2);
	}else if(set_point >= in5){
		*Kp = pid5[3];
	}
}
void CDC_ReceiveCallback(uint8_t *buf, uint32_t len)
{
	
	if(buf[0] == 45 && buf[1] == 46 && buf[2] == 47 && buf[3] == 48) {			
		if(first_start == 0) {
			first_start = 1;
			TIM3->CNT = 0;
		  HAL_TIM_Base_Start_IT(&htim3);
		  HAL_TIM_Encoder_Start(&htim1, TIM_CHANNEL_1 | TIM_CHANNEL_2);
			memcpy(para_data, sp_header, 4);
		}	
	  memcpy(&data_receive, buf, len);
	  set_speed = data_receive.data;
		selection = 0;
	}else if(buf[0] == 54 && buf[1] == 64 && buf[2] == 74 && buf[3] == 84) {
		if(first_start == 0) {
			first_start = 1;
			TIM3->CNT = 0;
		  HAL_TIM_Base_Start_IT(&htim3);
		  HAL_TIM_Encoder_Start(&htim1, TIM_CHANNEL_1 | TIM_CHANNEL_2);	 
      memcpy(para_data, po_header, 4);			
		}
	  memcpy(&data_receive, buf, len);
	  set_pos = data_receive.data;		
		ev_k3 = set_pos - pos; ev_k2 = ev_k3;
		selection = 1;
	}else if(buf[0] == 115 && buf[1] == 116 && buf[2] == 111 && buf[3] == 112) {
		TIM2->CCR1 = 0;
		HAL_TIM_Base_Stop_IT(&htim3);
		HAL_TIM_Encoder_Stop(&htim1, TIM_CHANNEL_1 | TIM_CHANNEL_2);
		u_k1 = 0; u = 0; e = 0; e_k1 = 0; e_k2 = 0; pulse_k1 = 0;	ev_k1 = 0; ev = 0; last_pulse = 0, current_pulse = 0, pos = 0;	
		TIM1->CNT = 0;
		TIM1->SR = 0;
		first_start = 0;
		memcpy(send, ack_header, 4);
		send[4] = 24; send[5] = 27;
		memcpy(&send[6], end_package, 2);
		CDC_Transmit_FS(send, 8);
	}else if(buf[0] == 69) {
    memcpy(&tune, &buf[1], 12);
		//Kpv = convert.real;
	}else if(buf[0] == 115 && buf[1] == 112 && buf[2] == 114 && buf[3] == 117) {	
		memcpy(send, ack_header, 4);
		send[4] = 's'; send[5] = 'p';
		memcpy(&send[6], end_package, 2);
		CDC_Transmit_FS(send, 8);
	}else if(buf[0] == 112 && buf[1] == 111 && buf[2] == 114 && buf[3] == 117) {
		memcpy(send, ack_header, 4);
		send[4] = 'p'; send[5] = 'o';
		memcpy(&send[6], end_package, 2);
		CDC_Transmit_FS(send, 8);
	}
	
	//memcpy(&curve, buf, len);
	//point += len;
	//length_check++;
}
void HAL_TIM_PeriodElapsedCallback(TIM_HandleTypeDef *htim)
{
  if(htim->Instance == TIM3) {
		//Kpv = 2.3;
		//Kpv = tune.kp; 
		//Kdv = tune.kd;
		
		
		//ki = tune.ki;
		//kd = tune.kd;
		
		
		if(selection == 1) {
			
			pulse = TIM1->CNT;
			count_pulse = (int16_t)pulse;
			
		  check = (uint8_t)(TIM1->SR &TIM_SR_UIF);
		  if (check == 0) {
			  speed = (pulse - pulse_k1) / T / 1496.0f*2*pi;
		  }else{
			TIM1->SR = 0;
			if((int16_t)(pulse) >= 0) 
				speed = (pulse - (int16_t)(pulse_k1)) / T / 1496.0f*2*pi;
			else
				speed = ((int16_t)(pulse) - pulse_k1) / T / 1496.0f*2*pi;
		  }	
								
		  
      current_pulse += (count_pulse - last_pulse);
      last_pulse = count_pulse;
      pos = (float)(current_pulse / 1496.0f * 2*pi);	
			ev = set_pos - pos;
			ev = (fabsf(ev) < 0.2f)?0.0f:ev;
			fuzzy_setpos(fabsf(set_pos), &Kpv);
			uv = Kpv*ev + (Kdv/T)*(ev - ev_k1);
			set_speed = uv;
			e = set_speed - speed;
			fuzzy(fabsf(set_speed), &kp, &ki, &th);			
			u = u_k1 + kp *(e - e_k1) + (ki*T/2.0f)*(e + e_k1) + (kd/T)*(e - 2*e_k1 + e_k2);
		  
			if(set_speed > 0) {
			  voltage = (u >= 11.83f)?11.83f:u;
		    voltage = (voltage < -th)?-th:voltage;
			  voltage = (voltage <= th && voltage>=0.0f)?th+voltage:voltage;
		  }else{
			  voltage = (u < -11.83f)?-11.83f:u;
			  voltage = (voltage > th)?th:voltage;	  
			  voltage = (voltage >= -th && voltage<=0.0f)?-th+voltage:voltage;
		  }			
			//******************************************************
	  }else if(selection == 0) {
			
			fuzzy(fabsf(set_speed), &kp, &ki, &th);
			pulse = TIM1->CNT;
		  check = (uint8_t)(TIM1->SR &TIM_SR_UIF);
		  if (check == 0) {
			  speed = (pulse - pulse_k1) / T / 1496.0f*2*pi;
		  }else{
			TIM1->SR = 0;
			if((int16_t)(pulse) >= 0) 
				speed = (pulse - (int16_t)(pulse_k1)) / T / 1496.0f*2*pi;
			else
				speed = ((int16_t)(pulse) - pulse_k1) / T / 1496.0f*2*pi;
		  }	
			
		  e = set_speed - speed;
			u = u_k1 + kp *(e - e_k1) + (ki*T/2.0f)*(e + e_k1) + (kd/T)*(e - 2*e_k1 + e_k2);
		  if(set_speed > 0) {
			  voltage = (u >= 11.83f)?11.83f:u;
		    voltage = (voltage < -th)?-th:voltage;
			  voltage = (voltage <= th && voltage>=0.0f)?th+voltage:voltage;
		  }else{
			  voltage = (u < -11.83f)?-11.83f:u;
			  voltage = (voltage > th)?th:voltage;	  
			  voltage = (voltage >= -th && voltage<=0.0f)?-th+voltage:voltage;
		  }
		}
			
		
		//voltage = (u >= -th && u < 0.0f)?-th:u;
		//set_speed_k2 = (set_speed != set_speed_k1)?set_speed_k1:set_speed_k2;
		/*if(set_speed_k2 <= 0.01f && set_speed_k2 >= -0.01f){
		  
		  voltage = (u >= -th && u < 0.0f)?-th+2.0f:u;
		}*/
		if(voltage > 0) {
			HAL_GPIO_WritePin(GPIOC, GPIO_PIN_0, GPIO_PIN_SET);
	    HAL_GPIO_WritePin(GPIOC, GPIO_PIN_2, GPIO_PIN_RESET);
		}else{
			HAL_GPIO_WritePin(GPIOC, GPIO_PIN_0, GPIO_PIN_RESET);
	    HAL_GPIO_WritePin(GPIOC, GPIO_PIN_2, GPIO_PIN_SET);
		}
		pwm_out = (uint16_t)(fabsf(voltage) / 11.83f * 1023.0f);
		TIM2->CCR1 = pwm_out;
		
	 if(selection == 1) {
			data_sp.data = pos;
		  data_sp.set = set_pos;
		}else if(selection == 0){
			data_sp.data = speed;
		  data_sp.set = set_speed;
		}		
		data_sp.voltage = voltage;
		data_sp.kp = kp;
		data_sp.ki = ki;
		data_sp.kd = th;
		memcpy(&para_data[4], &data_sp, 24);
		CDC_Transmit_FS(para_data, 30);
		
		
		//ev_k3 = ev_k2;
		//ev_k2 = ev_k1;
    ev_k1 = ev;		
		u_k1 = u;
		e_k2 = e_k1;
		e_k1 = e;
		pulse_k1 = pulse;
		set_speed_k1 = set_speed;

			
      
      			
		
	}
  
}

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */

  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{ 
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     tex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */

/************************ (C) COPYRIGHT STMicroelectronics *****END OF FILE****/
