using System;

public class TemperatureChangedEventArgs : EventArgs
{
    public double CurrentTemperature { get; set; }
}

public delegate void TemperatureChangedEventHandler(object sender, TemperatureChangedEventArgs e);

public class WeatherStation
{
    public event TemperatureChangedEventHandler TemperatureChanged;

    private double _currentTemperature;

    public double CurrentTemperature
    {
        get { return _currentTemperature; }
        set
        {
            if (value != _currentTemperature)
            {
                _currentTemperature = value;
                OnTemperatureChanged(new TemperatureChangedEventArgs { CurrentTemperature = _currentTemperature });
            }
        }
    }

    protected virtual void OnTemperatureChanged(TemperatureChangedEventArgs e)
    {
        TemperatureChanged?.Invoke(this, e);
    }
}

public class TemperatureDisplay
{
    public void Subscribe(WeatherStation weatherStation)
    {
        weatherStation.TemperatureChanged += WeatherStation_TemperatureChanged;
    }

    private void WeatherStation_TemperatureChanged(object sender, TemperatureChangedEventArgs e)
    {
        Console.WriteLine($"Current temperature is {e.CurrentTemperature} degrees Celsius.");
    }
}
