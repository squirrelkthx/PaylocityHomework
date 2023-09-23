// note: put paycheck calculation in the UI with the idea that individual customers/instances of the payroll app may have different customer specific settings, like number of paychecks per year
// along with other configuration that could affect the calc.  By having this logic in the UI individual instances of the app could pull different configurations while still all consuming the same property from the API: salary
// if paychecks were calculated in the API then it would need to be passed the configuration or it would have to be included in the data withen the API
export const calcPaycheck = (
  salary: number,
  dependentCount: number,
  currentAge: number
): number => {
  let salaryForPaycheck = salary;
  salaryForPaycheck = salaryForPaycheck - 1000 * 12; // $1000 for benefits per month
  if (dependentCount > 0) {
    salaryForPaycheck = salaryForPaycheck - dependentCount * 600 * 12; // $600 per dependent per month
  }
  if (salary > 80000) {
    salaryForPaycheck -= salary * 0.02; // if over $80,000 a year additional 2% to benefits
  }
  if (currentAge > 50) {
    salaryForPaycheck -= 200 * 12; // employees over 50 incur $200 per month to benefits
  }

  let paycheck: number = salaryForPaycheck / 26; // 26 paychecks per year
  return paycheck;
};
