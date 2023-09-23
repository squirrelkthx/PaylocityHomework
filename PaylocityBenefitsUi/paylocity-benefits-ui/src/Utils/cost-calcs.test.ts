import { calcPaycheck } from "./cost-calcs";

describe('testing cost-calcs file', () => {
    test('calcPaycheck should return expected values based on inputs', () => {
      expect(calcPaycheck(30000, 2, 30)).toBe(138.46153846153845);
      expect(calcPaycheck(40000, 0, 30)).toBe(1076.923076923077);
      expect(calcPaycheck(50000, 0, 60)).toBe(1369.2307692307693);
      expect(calcPaycheck(90000, 0, 60)).toBe(2838.4615384615386);
      expect(calcPaycheck(90000, 1, 40)).toBe(2653.846153846154);
      expect(calcPaycheck(90000, 6, 40)).toBe(1269.2307692307693);
      expect(calcPaycheck(90000, 0, 40)).toBe(2930.769230769231);
    });
  });
