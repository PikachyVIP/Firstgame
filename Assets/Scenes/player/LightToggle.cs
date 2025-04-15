using UnityEngine;

public class LightToggle : MonoBehaviour
{
    private Light myLight; // Ссылка на компонент Light
    private bool isLightOn = true; // Флаг состояния света

    void Start()
    {
        // Получаем компонент Light у текущего объекта
        myLight = GetComponent<Light>();
        
        // Убедимся, что свет включён при старте
        myLight.intensity = 2f;
    }

    void Update()
    {
        // Если нажата клавиша F
        if (Input.GetKeyDown(KeyCode.F))
        {
            // Переключаем состояние света
            isLightOn = !isLightOn;
            
            // Устанавливаем интенсивность в зависимости от состояния
            myLight.intensity = isLightOn ? 2f : 0f;
        }
    }
}